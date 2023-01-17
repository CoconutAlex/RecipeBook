import { ReactElement, JSXElementConstructor, ReactFragment, ReactPortal, useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Container, Divider, Form, Icon, Label, List, TextArea } from 'semantic-ui-react';
import { Difficulty, Ingredient } from '../../../app/models/recipe';

export default function RecipeDetails() {
    const location = useLocation();
    const { props } = location.state;

    const [hasScrolled, setHasScrolled] = useState(false);

    useEffect(() => {
        if (!hasScrolled) {
            window.scrollTo(0, 0);
            setHasScrolled(true);
        }
    }, [hasScrolled]);

    return (
        <div>
            <Container textAlign='right'>
                <div><Icon name='users' /> {` Portions: ${props.portions} `}</div>
                <div><Icon name='wait' /> {` Duration: ${props.duration}' `} </div>
                <div><Icon name='chart bar outline' /> {`Difficulty: ${Difficulty[props.difficulty]} `}</div>
            </Container>
            <Container textAlign='center' >
                <b>{props.title}</b>
            </Container>
            <Container textAlign='center' style={{ 'padding-top': '15px' }}>
                <img src={`/assets/recipesImages/${props.imageName}.png`} height='350px' />
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
                    {
                        props.description ? props.description.split('\n').map((place: string) => <p> {place} </p>) : ''
                    }
                </p>
            </Container>
        </div >
    )
}