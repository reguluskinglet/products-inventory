/* eslint-disable react/prefer-stateless-function */
import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import { Container, Row } from 'shards-react';
import { ApplicationState } from '../../store';
import * as UserStore from '../../store/UserStore';

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class Login extends React.Component<Props, any> {
  public constructor(props) {
    super(props);

    this.state = {
      login: { value: '' },
      password: { value: '' },
    };
  }

  private isValidForm = () => {
    const { login, password } = this.state;
    return this.validate(login.value) && this.validate(password.value);
  };

  private validate = (value: string) => {
    return value && value.length > 0;
  }

  public render() {
    return (
      <Container className="login-container">
        <Row />
      </Container>
    );
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
