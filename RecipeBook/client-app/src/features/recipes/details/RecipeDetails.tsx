import { useLocation } from 'react-router-dom';
import { Container, Divider, Icon, List } from 'semantic-ui-react';
import { Difficulty, Ingredient } from '../../../app/models/recipe';

export default function RecipeDetails() {
    const location = useLocation();
    const { props } = location.state;

    return (
        <div>
            <Container textAlign='right'>
                <div><Icon name='users' /> {` Portions: ${props.portions} `}</div>
                <div><Icon name='wait' /> {` Duration: ${props.duration}' `} </div>
                <div><Icon name='chart bar outline' /> {`Difficulty: ${Difficulty[props.difficulty]} `}</div>
            </Container>
            <Container textAlign='center' >
                {props.title}
            </Container>
            <Container textAlign='left'>
                <label>Ingredients List</label>
                <List bulleted>
                    {
                        (props.ingredientList.length)
                            ? props.ingredientList.map((item: Ingredient) => (
                                <List.Item key={item.id}>
                                    <div >{item.ingredientName.name} </div>
                                </List.Item>
                            ))
                            : <div>Empty List ...</div>
                    }
                </List>
            </Container>
            <Container textAlign='justified'>
                
                <Divider />
                <p>
                    {props.description}
                </p>
            </Container>
        </div>
    )
}