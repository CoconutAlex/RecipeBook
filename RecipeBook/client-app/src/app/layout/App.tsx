import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
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
