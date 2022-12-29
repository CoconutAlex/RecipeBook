import { createBrowserRouter, RouteObject } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import RecipeDashboard from '../../features/recipes/dashboard/RecipeDashboard';
import RecipeDetails from '../../features/recipes/details/RecipeDetails';
import EditRecipeForm from '../../features/recipes/form/EditRecipeForm';
import NewRecipeForm from '../../features/recipes/form/NewRecipeForm';
import App from "../layout/App";
import NavBar from '../layout/NavBar';

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'recipes', element: <RecipeDashboard /> },
            { path: 'details', element: <RecipeDetails /> },
            { path: 'newRecipe', element: <NewRecipeForm /> },
            { path: 'editRecipe', element: <EditRecipeForm /> },
        ]
    }
]

export const router = createBrowserRouter(routes);