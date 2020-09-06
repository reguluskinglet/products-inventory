import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import { ApplicationState } from '../../store';
import * as UserStore from '../../store/UserStore';
import { Container, Row } from "shards-react";

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
    properties: any;
}

class Login extends React.Component<Props, any> {
    public render() {
        return (
            <Container className="login-container">
                <Row>

                </Row>
            </Container>
        )
    }
}

const provider = provide(
    (state: ApplicationState) => (
      {
        ...state.user,
      }
    ),
    {
      ...UserStore.actionCreators,
    },
).withExternalProps<IExternalProps>();
  
type Props = typeof provider.allProps;

export default provider.connect(Login);
