import { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Container, Divider, Grid, GridColumn, Icon, Label, List } from 'semantic-ui-react';
import { Difficulty, Ingredient } from '../../../app/models/recipe';

export interface DescriptionItem {
    key: string,
    value: string
}

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

    var descriptionInSteps = props.description.split(/\r?\n\n/);
    var defaultListOfDescription = [];
    for (let i = 0; i < descriptionInSteps.length - 1; i++) {
        defaultListOfDescription.push({ key: (i + 1).toString(), value: descriptionInSteps[i].trim() });
    }

    const [listOfDescription, setListOfDescriptions] = useState<DescriptionItem[]>(defaultListOfDescription);

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
            <Container textAlign='center' style={{ 'paddingTop': '15px' }}>
                <img src={`/assets/recipesImages/${props.imageName}.png`} height='200px' />
            </Container>
            <Container textAlign='left'>
                <Container >
                    <label>Ingredients</label>
                </Container>
                <Container>
                    {
                        <List horizontal >
                            {
                                (props.ingredientList.length)
                                    ? props.ingredientList.map((item: Ingredient) => (
                                        <List.Item key={item.id}>
                                            <Label as='a' color='blue' size='medium' tag>
                                                {item.ingredientName.name}
                                            </Label>
                                        </List.Item>
                                    ))
                                    : <List.Item> Empty List ... </List.Item>
                            }
                        </List>
                    }
                </Container>
            </Container>
            <Container textAlign='justified'>
                <Divider />
                {
                    (listOfDescription.length > 0) ? <Label size='medium'>Let's Cook...</Label> : ''

                }
                <Container textAlign='justified' style={{ 'marginBottom': '20px' }} >
                    <Container>
                        <List>
                            {
                                (listOfDescription.length > 0)
                                && listOfDescription.map((item) => (
                                    <List.Item key={item.key} >
                                        <Grid columns={2} >
                                            <Grid.Row>
                                                <GridColumn width={'1'} textAlign='right' verticalAlign='middle'>
                                                    <Label circular style={{ 'verticalAlign': 'baseline', 'backgroundColor': '#BC6F6F', 'color': 'white' }}>{item.key}</Label>
                                                </GridColumn>
                                                <GridColumn width={14} style={{ 'paddingLeft': '1px' }}>
                                                    <Label size='large' pointing={'left'} style={{ 'whiteSpace': 'pre-wrap', 'wordBreak': 'break-word', 'backgroundColor': '#BC6F6F', 'color': 'white' }}>{item.value}</Label>
                                                </GridColumn>
                                            </Grid.Row>

                                        </Grid>
                                    </List.Item>
                                ))
                            }
                        </List>
                    </Container>
                </Container>
            </Container>
        </div >
    )
}