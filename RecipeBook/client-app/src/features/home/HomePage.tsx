import { Container } from "semantic-ui-react";

export default function HomePage() {
    return (
        <Container style={{ marginTop: '7em' }}>
            <h1>Recipe Book</h1>
            <p>
                Recipe Book to Write in Your Own Recipes
            <Container textAlign='center'>
                <img height={400} src='/assets/Logo.png' />
            </Container>
            </p>
        </Container>
    )
}