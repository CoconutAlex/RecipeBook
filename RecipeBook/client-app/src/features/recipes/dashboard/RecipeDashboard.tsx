import axios from "axios";
import { useEffect, useState } from "react";
import { Grid } from "semantic-ui-react";
import { Recipe } from "../../../app/models/recipe";
import RecipeList from "./RecipeList";

export default function RecipeDashboard() {

    const [recipes, setRecipes] = useState<Recipe[]>([]);

    useEffect(() => {
        axios.get<Recipe[]>('http://localhost:5118/Recipe/GetAllRecipes')
            .then(response => {
                setRecipes(response.data);
            })
    }, [])

    return (
        <Grid>
            <Grid.Column width='16'>
                {recipes &&
                    <RecipeList recipesList={recipes} />}
            </Grid.Column>
        </Grid>
    )
}