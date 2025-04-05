
import React from 'react';

import { Container, Row, Col } from 'react-bootstrap';

import MovieList from './MovieList';

export default function Movies() {
    return (
        <Container>
            <Row>
                <Col>
                    <h1>MegaHit Movies</h1>
                </Col>
            </Row>
            <MovieList />
        </Container>
    );
}

