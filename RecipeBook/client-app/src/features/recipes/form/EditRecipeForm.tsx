import axios from 'axios';
import { isEmpty } from 'lodash';
import { SetStateAction, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Button, Container, Form, Grid, GridColumn, Icon, Input, Label, List, Select } from 'semantic-ui-react'
import { Difficulty, IngredientName, IngredientQuantity } from '../../../app/models/recipe';
import RecipeDashboard from '../dashboard/RecipeDashboard';

export interface DropdownList {
    id: string,
    key: string,
    text: string,
    value: string
}

export interface IngredientItem {
    key: string,
    value: string,
    quantityid: string,
    nameid: string
}

export interface DescriptionItem {
    key: string,
    value: string
}

export default function EditRecipeForm() {

    const location = useLocation()
    const { props } = location.state

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
            quantities.push({ key: quantity.id, text: quantity.quantity, value: quantity.quantity, id: quantity.id })
        ))

        return quantities;
    }

    var [names] = useState<DropdownList[]>([]);
    function getIngredientsName() {
        names = [];
        ingredientsName.forEach(name => {
            names.push({ key: name.id, text: name.name, value: name.name, id: name.id });
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
            realText = realText.replace(/([A-Z])/g, ' $1').trim();

            difficulties.push({ key: propertyKey, text: realText, value: propertyValue });
        }
        return difficulties;
    }

    function getDefaultList() {
        var defaultList: any = [];

        props.ingredientList.forEach((item: any) => {
            let uniqueKey = item.ingredientQuantity.id + '-' + item.ingredientName.id;
            defaultList.push({ key: uniqueKey, value: `${item.ingredientQuantity.quantity} ${item.ingredientName.name}`, quantityid: item.ingredientQuantity.id, nameid: item.ingredientName.id });
        });

        return defaultList;
    }

    const onChangeDifficulty = (e: any, data: any) => {
        var find = data.options.find((item: { value: any; }) => {
            return item.value === data.value
        })
        setNewDifficulty(find.key);
    };

    var [insertedIngredients, setinsertedIngredients] = useState<IngredientItem[]>(getDefaultList());
    const addIngredient = (quantity: string, quantityId: string, name: string, nameId: string) => {
        const newIngredients = [...insertedIngredients];
        let uniqueKey = quantityId + '-' + nameId;
        newIngredients.push({ key: uniqueKey, value: `${quantity} ${name}`, quantityid: quantityId, nameid: nameId });
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

        if (isEmpty(selectedId)) {
            var withoutSpaces = data.value.split(" ").join("");
            setSelectedQuantity({ id: generateGUID(), quantity: withoutSpaces });
        } else {
            setSelectedQuantity({ id: selectedId, quantity: data.value });
        }
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

        if (isEmpty(selectedId)) {
            setSelectedName({ id: generateGUID(), name: data.value });
        } else {
            setSelectedName({ id: selectedId, name: data.value });
        }
    };

    function generateGUID() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    var descriptionInSteps = props.description.split(/\r?\n\n/);
    var defaultListOfDescription = [];
    for (let i = 0; i < descriptionInSteps.length - 1; i++) {
        defaultListOfDescription.push({ key: (i + 1).toString(), value: descriptionInSteps[i].trim() });
    }

    const [listOfDescription, setListOfDescriptions] = useState<DescriptionItem[]>(defaultListOfDescription);

    const [currentStep, setCurrentStep] = useState<number>(listOfDescription.length + 1);
    const [partOfDescription, setPartOfDescription] = useState<string>();
    const addPartOfDescription = (e: any, data: any) => {
        setPartOfDescription(data.value);
    };

    const [someDescription, setSomeDescription] = useState<string>(props.description);
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
                setPartOfDescription(listOfDescription.length > 1 ? listOfDescription[1].value : '');
            } else {
                setCurrentStep(step - 1);
                setPartOfDescription(listOfDescription[step - 2].value);
            }
        }
    };

    const [newTitle, setNewTitle] = useState('');
    const [newPortions, setNewPortions] = useState('');
    const [newDuration, setNewDuration] = useState('');
    const [newImageName, setNewImageName] = useState('');
    const [newDifficulty, setNewDifficulty] = useState('');
    useEffect(() => {
        setNewTitle(props.title);
        setNewPortions(props.portions);
        setNewDuration(props.duration);
        setNewImageName(props.imageName);
        setNewDifficulty(props.difficulty);
    }, [props.title, props.portions, props.duration, props.imageName, props.difficulty])

    const handleSubmit = () => {

        var finalDescription = '';
        listOfDescription.forEach(item => {
            finalDescription += `${item.value}\n\n `;
        })

        var finalIngredientslist = [] as any;
        insertedIngredients.forEach(item => {
            finalIngredientslist.push({
                ingredientQuantityId: item.quantityid,
                ingredientQuantity: item.value.split(' ')[0],
                ingredientNameId: item.nameid,
                ingredientName: item.value.split(' ').slice(1, item.value.split(' ').length).join(' ')
            });
        });

        const request = {
            title: newTitle,
            portions: parseInt(newPortions),
            duration: parseInt(newDuration),
            imageName: newImageName,
            difficulty: parseInt(newDifficulty),
            steps: listOfDescription.length,
            ingredients: finalIngredientslist,
            description: finalDescription
        }

        axios.put(`http://localhost:5118/Recipe/UpdateRecipe/${props.id}`, request)
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
                value={newTitle}
                onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewTitle(e.target.value)}
            />
            <Form.Group widths='equal'>
                <Form.Field
                    id='form-input-control-portions'
                    control={Input}
                    label='Portions'
                    placeholder='Portions'
                    // defaultValue={props.portions}
                    value={newPortions}
                    type='number'
                    className='without-spinner'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewPortions(e.target.value)}
                />
                <Form.Field
                    id='form-input-control-duration'
                    control={Input}
                    label='Duration'
                    placeholder='Duration'
                    // defaultValue={props.duration}
                    value={newDuration}
                    type='number'
                    className='without-spinner'
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewDuration(e.target.value)}
                />
                <Form.Field
                    id='form-input-control-image-name'
                    control={Input}
                    label='Image Name'
                    placeholder='Image name'
                    // defaultValue={props.imageName}
                    value={newImageName}
                    onChange={(e: { target: { value: SetStateAction<string>; }; }) => setNewImageName(e.target.value)}
                />
                <Form.Field
                    control={Select}
                    options={getDifficultiesEnum()}
                    label='Difficulty'
                    placeholder='Difficulty'
                    search
                    // defaultValue={Difficulty[props.difficulty]}
                    value={Difficulty[parseInt(newDifficulty)]}
                    onChange={onChangeDifficulty}
                />
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
                            onClick={() => addIngredient((selectedQuantity) ? selectedQuantity.quantity : '', (selectedQuantity) ? selectedQuantity.id : '', (selectedName) ? selectedName.name : '', (selectedName) ? selectedName.id : '')}>
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
            {/* <Form.Group>
                <Form.Field
                    width='4'
                    control={Input}
                    placeholder='New Ingredient Quantity'
                    onChange={handleSelectedIngredientQuantity}
                />
                <Form.Field
                    width='4'
                    control={Input}
                    placeholder='New Ingredient Name'
                    onChange={handleSelectedIngredientName}
                />
            </Form.Group> */}
            <Form.Field id='form-input-control-ingredients'>
                <label>Ingredients List</label>
                {
                    <List horizontal >
                        {
                            (insertedIngredients.length)
                                ? insertedIngredients.map((item) => (
                                    <List.Item key={item.key} quantityid={item.quantityid} nameid={item.nameid}>
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
                <>
                    <Container>
                        <label style={{ fontWeight: 'bold' }}>Description</label>
                    </Container>
                    <Container className='segment' style={{ background: 'transparent' }}>
                        <Container>
                            <List selection >
                                {
                                    (listOfDescription.length > 0)
                                    && listOfDescription.map((item) => (
                                        <List.Item key={item.key} onClick={() => { setCurrentStep(parseInt(item.key)); setPartOfDescription(item.value); }}>
                                            <Grid columns={2} style={{ 'gridColumnGap': '0' }} >
                                                <Grid.Row >
                                                    <GridColumn width={1} textAlign='right' verticalAlign='middle'>
                                                        <Label circular style={{ verticalAlign: 'baseline', color: '#615f5f', backgroundColor: 'white' }}>{item.key}</Label>
                                                    </GridColumn>
                                                    <GridColumn width={14} style={{ 'paddingLeft': '1px' }}>
                                                        <Label size='large' pointing={'left'} style={{ whiteSpace: 'pre-wrap', wordBreak: 'break-word', color: '#615f5f', backgroundColor: 'white' }}>{item.value}</Label>
                                                    </GridColumn>
                                                </Grid.Row>
                                            </Grid>
                                        </List.Item>
                                    ))
                                }
                            </List>
                        </Container>
                    </Container>
                </>
            }

            <Grid>
                <Grid.Column textAlign="center">
                    <Button
                        type='submit'
                        primary
                        onClick={handleSubmit}
                        className={(newTitle && parseInt(newPortions) > 0 && parseInt(newDuration) && newImageName && parseInt(newDifficulty) && listOfDescription.length && insertedIngredients.length > 0 && listOfDescription.length > 0) ? '' : 'disabled'}
                    >Submit</Button>
                </Grid.Column>
            </Grid>

        </Form>
    )
}