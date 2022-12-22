import logo from './logo.svg';
import './App.css';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { Header } from 'semantic-ui-react';
import List from 'semantic-ui-react/dist/commonjs/elements/List';
import ListItem from 'semantic-ui-react/dist/commonjs/elements/List/ListItem';
import Button from 'semantic-ui-react/dist/commonjs/elements/Button';

function App() {
  const [recipies, setRecipies] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5118/Recipe/GetAllRecipies')
      .then(response => {
        setRecipies(response.data);
      })
  }, [])

  return (
    <div>
      <Header as='h2' icon='food' content='RecipeBook' />
      <List>
        {recipies.map((recipe: any) => (
          <List.Item key={recipe.id}>
            {recipe.title}
          </List.Item>
        ))}
      </List>
    </div>
  );
}

export default App;
