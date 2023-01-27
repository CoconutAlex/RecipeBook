import axios from 'axios';
import { SetStateAction, useEffect, useState } from 'react';
import { Button, Container, Form, Grid, GridColumn, Icon, Input, Label, List, Select, TextArea } from 'semantic-ui-react'
import { Difficulty, IngredientName, IngredientQuantity } from '../../../app/models/recipe';

export interface DropdownList {
    key: string,
    text: string,
    value: string
}

export interface IngredientItem {
    key: string,
    value: string
}

export interface DescriptionItem {
    key: string,
    value: string
}

export default function NewRecipeForm() {

    const [hasScrolled, setHasScrolled] = useState(false);

    useEffect(() => {
        if (!hasScrolled) {
            window.scrollTo(0, 0);
            setHasScrolled(true);
        }
    }, [hasScrolled]);

    const [ingredientsQuantity, setIngredientsQuantity] = useState<IngredientQuantity[]>([]);
    const [ingredientsName, setIngredientsName] = useState<IngredientName[]>([]);
    const [selectedQuantity, setSelectedQuantity] = useState<IngredientQuantity>();
    const [selectedName, setSelectedName] = useState<IngredientName>();

    useEffect(() => {
        axios.get<IngredientQuantity[]>('http://localhost:5118/Ingredient/GetAllIngredientsQuantities')
            .then(response => {
                setIngredientsQuantity(response.data);
            });

        axios.get<IngredientName[]>('http://localhost:5118/Ingredient/GetAllIngredientsNames')
            .then(response => {
                setIngredientsName(response.data);
            });

    }, []);

    var [quantities] = useState<DropdownList[]>([]);
    function getIngredientsQuantity() {
        quantities = [];
        ingredientsQuantity.map((quantity) => (
            quantities.push({ key: quantity.id, text: quantity.quantity, value: quantity.quantity })
        ))

        return quantities;
    }

    var [names] = useState<DropdownList[]>([]);
    function getIngredientsName() {
        names = [];
        ingredientsName.forEach(name => {
            names.push({ key: name.id, text: name.name, value: name.name });
        });

        return names;
    }

    function getDifficultiesEnum() {
        var difficulties = [];

        for (var [propertyKey, propertyValue] of Object.entries(Difficulty)) {
            if (Number.isNaN(Number(propertyKey))) {
                continue;
            }

            var realText = propertyValue.toString();
            realText = realText.replace(/([A-Z])/g, ' $1').trim()

            difficulties.push({ key: propertyKey, text: realText, value: propertyValue });
        }
        return difficulties;
    }

    const onChangeDifficulty = (e: any, data: any) => {
        var find = data.options.find((item: { value: any; }) => {
            return item.value === data.value
        })
        setNewDifficulty(find.key);
    };

    var [insertedIngredients, setinsertedIngredients] = useState<IngredientItem[]>([]);
    const addIngredient = (quantity: string, name: string) => {
        const newIngredients = [...insertedIngredients];
        let uniqueKey = (Math.random() + 1).toString(36).substring(2);
        newIngredients.push({ key: uniqueKey, value: `${quantity} ${name}` });
        setinsertedIngredients(newIngredients);
    };

    function deleteAllIngredients() {
        setinsertedIngredients([]);
    }

    const removeIngredient = (removeItem: IngredientItem) => {
        const newIngredients = [...insertedIngredients];
        var index = newIngredients.indexOf(removeItem);
        if (index !== -1) {
            newIngredients.splice(index, 1);
            setinsertedIngredients(newIngredients);
        }
    };

    function handleSelectedIngredientQuantity(e: any, data: any) {
        var selectedId = '';

        ingredientsQuantity.every((item) => {
            if (item.quantity === data.value) {
                selectedId = item.id;
                return false;
            }
            return true;
        });

        setSelectedQuantity({ id: selectedId, quantity: data.value });
    };

    function handleSelectedIngredientName(e: any, data: any) {
        var selectedId = '';

        ingredientsName.every((item) => {
            if (item.name === data.value) {
                selectedId = item.id;
                return false;
            }
            return true;
        });

        setSelectedName({ id: selectedId, name: data.value });
    };

    const [currentStep, setCurrentStep] = useState<number>(1);

    const [partOfDescription, setPartOfDescription] = useState<string>();
    const addPartOfDescription = (e: any, data: any) => {
        setPartOfDescription(data.value);
    };

    const [listOfDescription, setListOfDescriptions] = useState<DescriptionItem[]>([]);
    const [someDescription, setSomeDescription] = useState<string>('');
    const addSomeDescription = () => {
        var counter: number = currentStep;

        var newListOfDescriptions = [...listOfDescription];
        newListOfDescriptions = [...newListOfDescriptions.slice(0, counter - 1), { key: counter.toString(), value: `${partOfDescription}` }, ...newListOfDescriptions.slice(counter)];
        setListOfDescriptions(newListOfDescriptions);
        setCurrentStep(counter + 1);
        setPartOfDescription('');
    };

    const removeSomeDescription = (step: number) => {
        const newListOfDescriptions = [...listOfDescription];
        if (step !== -1) {
            newListOfDescriptions.splice(step - 1, 1);
            var restArray = newListOfDescriptions.slice(step - 1, newListOfDescriptions.length);
            for (var i = step - 1; i < newListOfDescriptions.length; i++) {
                newListOfDescriptions[i].key = (i + 1).toString();
            }

            setListOfDescriptions(newListOfDescriptions);
            if (step - 1 <= 0) {
                setCurrentStep(1);
            } else {
                setCurrentStep(step - 1);
            }
            setPartOfDescription('');
        }
    };

    const [newTitle, setNewTitle] = useState('');
    const [newPortions, setNewPortions] = useState('');
    const [newDuration, setNewDuration] = useState('');
    const [newImageName, setNewImageName] = useState('');
    const [newDifficulty, setNewDifficulty] = useState('');

    const handleSubmit = () => {
        var finalDescription = '';
        listOfDescription.forEach(item => {
            finalDescription += `${item.value}\n\n `;
        })
        const request = {
            title: newTitle,
            portions: parseInt(newPortions),
            duration: parseInt(newDuration),
            imageName: newImageName,
            difficulty: parseInt(newDifficulty),
            steps: listOfDescription.length,
            ingredients: [{
                ingredientId: "BFD37128-DCAA-4707-FFD6-08DAE26FC1FC"
            }],
            description: finalDescription
        }

        axios.post('http://localhost:5118/Recipe/AddRecipe', request)
            .then(response => {
                alert("The response is: " + response);
            })
            .catch(error => {
                alert("The error is: " + error);
            });
    };

    function handleKeyPress(event: React.KeyboardEvent<HTMLInputElement>) {
        if (event.key === 'Enter') {
            event.preventDefault();
        }
    }

    return (
        <Form autoComplete="off" onKeyPress={handleKeyPress}>
            <Form.Field
                id='form-input-control-title'
                control={Input}
                label='Title'
                placeholder='Title'
                onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewTitle(e.target.value)}
            />
            <Form.Group widths='equal'>
                <Form.Field
                    id='form-input-control-portions'
                    control={Input}
                    label='Portions'
                    placeholder='Portions'
                    type='number'
                    className='without-spinner'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewPortions(e.target.value)}
                />
                <Form.Field
                    id='form-input-control-duration'
                    control={Input}
                    label='Duration'
                    placeholder='Duration'
                    type='number'
                    className='without-spinner'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewDuration(e.target.value)}
                />
                <Form.Field
                    id='form-input-control-image-name'
                    control={Input}
                    label='Image Name'
                    placeholder='Image name'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewImageName(e.target.value)}
                />
                <Form.Field
                    control={Select}
                    options={getDifficultiesEnum()}
                    label='Difficulty'
                    placeholder='Difficulty'
                    search
                    onChange={onChangeDifficulty}
                />
                {/* <Form.Field
                    id='form-input-control-steps'
                    control={Input}
                    label='Steps'
                    placeholder='Steps'
                    type='number'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewSteps(e.target.value)}
                    onKeyPress={handleKeyPress}
                    disabled
                /> */}
            </Form.Group>
            <Form.Group>
                <Form.Field
                    width='4'
                    control={Select}
                    options={getIngredientsQuantity()}
                    label='Ingredient Quantity'
                    placeholder='Ingredient Quantity'
                    search
                    onChange={handleSelectedIngredientQuantity}
                />
                <Form.Field
                    width='6'
                    control={Select}
                    options={getIngredientsName()}
                    label='Ingredient Name'
                    placeholder='Ingredient Name'
                    search
                    onChange={handleSelectedIngredientName}
                />
                <Grid>
                    <Grid.Column verticalAlign='bottom'>
                        <Button
                            positive
                            className={(selectedQuantity && selectedName) ? '' : 'disabled'}
                            onClick={() => addIngredient((selectedQuantity) ? selectedQuantity.quantity : '', (selectedName) ? selectedName.name : '')}>
                            <div><Icon name='check' /></div>
                        </Button>
                        {
                            (insertedIngredients.length > 1) &&
                            <Button
                                negative
                                className={(insertedIngredients.length > 0) ? '' : 'disabled'}
                                onClick={() => deleteAllIngredients()}>
                                <div>Delete all<Icon name='trash alternate outline' /></div>
                            </Button>
                        }

                    </Grid.Column>
                </Grid>
            </Form.Group>
            <Form.Field id='form-input-control-ingredients'>
                <label>Ingredients List</label>
                {
                    <List horizontal >
                        {
                            (insertedIngredients.length)
                                ? insertedIngredients.map((item) => (
                                    <List.Item key={item.key}>
                                        <Label as='a' color='blue' size='medium'>
                                            {item.value}
                                            <Icon name='close' id='mini-trash' color='black' onClick={() => removeIngredient(item)} />
                                        </Label>
                                    </List.Item>
                                ))
                                : <List.Item> Empty List ... </List.Item>
                        }
                    </List>
                }
            </Form.Field>
            <Form.Group>
                {/* <Form.Field
                    id='form-input-control-step'
                    control={Input}
                    label='Step'
                    placeholder='Step'
                    type='number'
                    onChange={addCurrentStep}
                    value={currentStep}
                /> */}

                <Form.TextArea
                    width='twelve'
                    label={<><Label >Description for Step</Label><Label circular style={{ 'backgroundColor': '#BC6F6F', 'color': 'white', 'marginBottom': '10px' }}>{currentStep}</Label></>}
                    placeholder='Description for Step'
                    onChange={addPartOfDescription}
                    value={partOfDescription}
                />
                <Grid>
                    <Grid.Column verticalAlign='bottom' className={partOfDescription ? '' : 'disabled-column'}>
                        <Button icon={{ id: 'add-some-description', name: 'add', size: 'big', color: 'green' }} onClick={() => (currentStep && partOfDescription) && addSomeDescription()} className={'button-without-background'} />
                    </Grid.Column>
                </Grid>
                <Grid>
                    <Grid.Column verticalAlign='bottom' className={partOfDescription ? '' : 'disabled-column'}>
                        <Button icon={{ id: 'remove-some-description', name: 'minus', size: 'big', color: 'red' }} onClick={() => (currentStep && partOfDescription) && removeSomeDescription(currentStep)} className={'button-without-background'} />
                    </Grid.Column>
                </Grid>
            </Form.Group>

            {
                (listOfDescription.length > 0) &&
                <label style={{ fontWeight: 'bold' }}>Description</label> &&
                <Container textAlign='justified' className='segment' style={{ marginBottom: '20px', border: '1px solid black', background: 'transparent' }}>
                    <Container>
                        <List selection >
                            {
                                (listOfDescription.length > 0)
                                && listOfDescription.map((item) => (
                                    <List.Item key={item.key} onClick={() => { setCurrentStep(parseInt(item.key)); setPartOfDescription(item.value); }}>
                                        <Grid columns={2} style={{ 'gridColumnGap': '0' }} >
                                            <Grid.Row>
                                                <GridColumn width={1} textAlign='right' verticalAlign='middle'>
                                                    <Label circular style={{ verticalAlign: 'baseline', color: '#615f5f', backgroundColor: 'white' }}>{item.key}</Label>
                                                </GridColumn>
                                                <GridColumn width={14} style={{ 'paddingLeft': '1px' }}>
                                                    <Label size='large' pointing={'left'} style={{ 'whiteSpace': 'pre-wrap', 'wordBreak': 'break-word', color: '#615f5f', backgroundColor: 'white' }}>{item.value}</Label>
                                                </GridColumn>
                                            </Grid.Row>
                                        </Grid>
                                    </List.Item>
                                ))
                            }
                        </List>
                    </Container>
                </Container>
            }



            <Grid>
                <Grid.Column textAlign="center">
                    <Button
                        type='submit'
                        primary
                        onClick={handleSubmit}
                        className={(newTitle && parseInt(newPortions) > 0 && parseInt(newDuration) > 0 && newImageName && parseInt(newDifficulty) >= 0 && listOfDescription.length && insertedIngredients.length > 0 && listOfDescription.length > 0) ? '' : 'disabled'}
                    >Submit</Button>
                </Grid.Column>
            </Grid>
        </Form >
    )
}