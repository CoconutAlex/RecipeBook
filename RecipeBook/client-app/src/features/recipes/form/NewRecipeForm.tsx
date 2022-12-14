import axios from 'axios';
import { useEffect, useState } from 'react';
import { Button, Form, Grid, Icon, Input, List, Select } from 'semantic-ui-react'
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

export default function NewRecipeForm() {

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

    var [insertedIngredients, setinsertedIngredients] = useState<IngredientItem[]>([]);
    const addIngredient = (quantity: string, name: string) => {
        const newIngredients = [...insertedIngredients];
        let uniqueKey = (Math.random() + 1).toString(36).substring(2);
        newIngredients.push({ key: uniqueKey, value: `${quantity} ${name}` });
        setinsertedIngredients(newIngredients);
    };

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

    const [partOfDescription, setPartOfDescription] = useState<string>();
    const addPartOfDescription = (e: any, data: any) => {
        setPartOfDescription(data.value);
    };

    const [someDescription, setSomeDescription] = useState<string>('');
    const addSomeDescription = () => {
        setSomeDescription(`${(someDescription) ? someDescription + '\n\n\n' : ''} ${currentStep} ${partOfDescription}`);
        var counter: number = currentStep;
        setCurrentStep(++counter);
        setPartOfDescription('');
    };

    const [currentStep, setCurrentStep] = useState<number>(0);
    const addCurrentStep = (e: any, data: any) => {
        setCurrentStep(data.value);
    };

    return (
        <Form autoComplete="off">
            <Form.Field
                id='form-input-control-title'
                control={Input}
                label='Title'
                placeholder='Title'

            />
            <Form.Group widths='equal'>
                <Form.Field
                    id='form-input-control-portions'
                    control={Input}
                    label='Portions'
                    placeholder='Portions'
                    type='number'
                    className='without-spinner'
                />
                <Form.Field
                    id='form-input-control-duration'
                    control={Input}
                    label='Duration'
                    placeholder='Duration'
                    type='number'
                    className='without-spinner'
                />
                <Form.Field
                    id='form-input-control-image-name'
                    control={Input}
                    label='Image Name'
                    placeholder='Image name'
                />
                <Form.Field
                    control={Select}
                    options={getDifficultiesEnum()}
                    label='Difficulty'
                    placeholder='Difficulty'
                    search
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
                        <Button positive onClick={() => addIngredient((selectedQuantity) ? selectedQuantity.quantity : '', (selectedName) ? selectedName.name : '')}><div><Icon name='check' /></div></Button>
                    </Grid.Column>
                </Grid>
            </Form.Group>
            <Form.Field id='form-input-control-ingredients'>
                <label>Ingredients List</label>
                <List bulleted>
                    {
                        (insertedIngredients.length)
                            ? insertedIngredients.map((item) => (
                                <List.Item key={item.key}>
                                    <div >{item.value} <Icon color='red' id='mini-trash' name='trash alternate outline' onClick={() => removeIngredient(item)} /></div>
                                </List.Item>
                            ))
                            : <div>Empty List ...</div>
                    }
                </List>
            </Form.Field>
            <Form.Group>
                <Form.Field
                    id='form-input-control-step'
                    control={Input}
                    label='Step'
                    placeholder='Step'
                    type='number'
                    onChange={addCurrentStep}
                    defaultValue='1'
                    value={currentStep}
                />
                <Form.TextArea
                    width='twelve'
                    label='Description for Step'
                    placeholder='Description for Step'
                    onChange={addPartOfDescription}
                    value={partOfDescription}
                />
                <Grid>
                    <Grid.Column verticalAlign='bottom'>
                        <Icon id='add-some-description' name='add' size='big' onClick={() => addSomeDescription()} ></Icon>
                    </Grid.Column>
                </Grid>
            </Form.Group>
            <Form.TextArea label='Description' placeholder='Description' value={someDescription} style={{ minHeight: 1000 }} />
            <Grid>
                <Grid.Column textAlign="center">
                    <Button type='submit' primary>Submit</Button>
                </Grid.Column>
            </Grid>
        </Form>
    )
}