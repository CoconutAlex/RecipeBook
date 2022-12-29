import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import { Button, Icon, Item, Segment } from 'semantic-ui-react';
import { Difficulty, Recipe } from '../../../app/models/recipe';

interface Props {
    recipesList: Recipe[];
}

export default function RecipeList({ recipesList }: Props) {
    const navigate = useNavigate();

    const [recipes, setRecipes] = useState<Recipe[]>([]);

    useEffect(() => {
        setRecipes(recipesList);
    }, [recipesList])

    const DeleteRecipe = (id: string) => {
        axios.delete(`http://localhost:5118/Recipe/DeleteRecipe/${id}`)
            .then(response => {
                const newRecipes = [...recipes];
                var index = -1;
                newRecipes.every((item) => {
                    index++;

                    if (item.id == response.data.id) {
                        return false;
                    }

                    return true;
                });

                if (index !== -1) {
                    newRecipes.splice(index, 1);
                    setRecipes(newRecipes);
                }
            })
    }

    return (
        <Segment>
            <Item.Group divided>
                {recipes.map((recipe) => (
                    <Item key={recipe.id}>
                        <Item.Image size='small' src={`/assets/recipesImages/${recipe.imageName}.png`} />

                        <Item.Content>
                            <Item.Header>{recipe.title}</Item.Header>
                            <Item.Description>
                                <div><Icon name='users' /> {` Portions: ${recipe.portions} `}</div>
                                <div><Icon name='wait' /> {` Duration: ${recipe.duration}' `} </div>
                                <div><Icon name='chart bar outline' /> {`Difficulty: ${Difficulty[recipe.difficulty]} `}</div>
                            </Item.Description>
                            <Item.Extra>
                                <Button floated='left' primary onClick={() => navigate("/contact")}>View Recipe</Button>
                                <Button negative floated='right' onClick={() => DeleteRecipe(recipe.id)}><div><Icon name='trash alternate outline' /></div></Button>
                                <Button
                                    as={NavLink} to='/editRecipe'
                                    state={{
                                        props: {
                                            title: recipe.title,
                                            portions: recipe.portions,
                                            duration: recipe.duration,
                                            imageName: recipe.imageName,
                                            difficulty: recipe.difficulty,
                                            ingredientList: recipe.ingredients,
                                            steps: recipe.steps,
                                            description: recipe.description
                                        }
                                    }}
                                    name='newRecipe'
                                    basic color='yellow' floated='right'>
                                    <div>Edit Recipe <Icon name='edit outline' /></div>
                                </Button>
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
}