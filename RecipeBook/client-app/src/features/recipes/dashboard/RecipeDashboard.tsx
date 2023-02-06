import axios from "axios";
import { useEffect, useState } from "react";
import { Grid } from "semantic-ui-react";
import { Recipe } from "../../../app/models/recipe";
import RecipeList from "./RecipeList";

export default function RecipeDashboard() {

    const [isLoading, setIsLoading] = useState(false);
    const [recipes, setRecipes] = useState<Recipe[]>([]);

    useEffect(() => {
        setIsLoading(true);
        axios.get<Recipe[]>('http://localhost:5118/Recipe/GetAllRecipes')
            .then(response => {
                setRecipes(response.data);
                setIsLoading(false);
            })
    }, [])

    if (isLoading) {
        return (
            <div className="loader">
                <div className="spinner"></div>
            </div>
        );
    }

    return (
        <Grid>
            <Grid.Column width='16'>
                {recipes &&
                    <RecipeList recipesList={recipes} />}
            </Grid.Column>
        </Grid>
    )
}