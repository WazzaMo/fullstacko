import React from 'react';
import {
    Card,
    CardBody,
    CardTitle,
    CardText,
    CardImg
} from 'react-bootstrap';

import SadDog from './sad-dog-171x256--pexels-ivan-samkov-6291579.webp';

export default function ErrorBox({ message }: { message: string }) {
    return (
        <Card style={{ width: '171px' }}>
            <CardImg src={SadDog} height={256} width={171} />
            <CardBody>
                <CardTitle>Whoops...</CardTitle>
                <CardText>{message}</CardText>
            </CardBody>
        </Card>
    );
}