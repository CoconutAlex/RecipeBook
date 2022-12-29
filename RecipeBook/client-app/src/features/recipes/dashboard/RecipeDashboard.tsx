import axios from "axios";
import { useEffect, useState } from "react";
import { Grid } from "semantic-ui-react";
import { Recipe } from "../../../app/models/recipe";
import RecipeList from "./RecipeList";

export default function RecipeDashboard() {

    return (
        <Grid>
            <Grid.Column width='16'>
                <RecipeList />
            </Grid.Column>
        </Grid>
    )
}