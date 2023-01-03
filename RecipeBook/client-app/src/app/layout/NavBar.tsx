import { NavLink } from 'react-router-dom';
import { Button, Icon, Menu, Segment } from 'semantic-ui-react';
import CustomSearch from './CustomSearch';

export default function NavBar() {
    return (
        <Segment inverted>
            <Menu inverted fixed='top'>
                <Menu.Item as={NavLink} to='/' header>
                    <img src='/assets/Logo.png' alt='logo' />
                </Menu.Item>
                <Menu.Item as={NavLink} to='/recipes'
                    name='recipes'
                />
                <Menu.Item >
                    <Button as={NavLink} to='/newRecipe'
                        name='newRecipe' positive>
                        <div>Add Recipe <Icon name='add' /></div>
                    </Button>
                </Menu.Item>
                <Menu.Item position='right'>
                <CustomSearch />
                </Menu.Item>
            </Menu>
        </Segment>
    )
}