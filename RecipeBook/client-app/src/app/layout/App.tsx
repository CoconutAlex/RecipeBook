import { useEffect, useState } from 'react';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Recipe } from '../models/recipe';
import NavBar from './NavBar';
import RecipesDashboard from '../../features/recipes/dashboard/RecipeDashboard';
import { Outlet } from 'react-router-dom';

function App() {
  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '3em' }}>
        <Outlet />
      </Container>
    </>
  );
}

export default App;
