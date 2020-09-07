/* eslint-disable react/prefer-stateless-function */
import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Form,
  FormGroup,
  FormInput,
  Button
} from 'shards-react';
import { IApplicationState } from '../../store';
import { actionCreators } from '../../store/User/action';

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class Login extends React.Component<Props, any> {
  state: {
    login: { value: string; };
    password: { value: string; };
  };

  public constructor(props: any) {
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

  private onChange = (name: string, value: string) => {
    const isValid = this.validate(value);

    this.setState({
      [name]: {
        value,
      },
    });
    return isValid;
  };

  private onSubmit = (e: React.MouseEvent | React.KeyboardEvent) => {
    e.preventDefault();

    const { logIn } = this.props;
    const { login, password } = this.state;

    if (this.isValidForm()) {
      logIn(
        {
          login: login.value,
          password: password.value,
        },
        '/products',
      );
    }
  };

  public render() {
    return (
      <Container className="login-container">
        <Row>
          <Col>
            <Form style={{ maxWidth: "350px", marginTop: "20%", marginLeft: "auto", marginRight: "auto"}}>
              <Card>
                <CardHeader>Авторизация</CardHeader>
                <CardBody>
                  <FormGroup>
                    <label htmlFor="#loginInput">Электронный адрес</label>
                    <FormInput id="#loginInput" placeholder="" onChange={(e: any) => this.onChange('login', e.target.value)} />
                  </FormGroup>
                  <FormGroup>
                    <label htmlFor="#passwordInput">Пароль</label>
                    <FormInput type="password" id="#passwordInput" placeholder="" onChange={(e: any) => this.onChange('password', e.target.value)} />
                  </FormGroup>
                </CardBody>
                <CardFooter>
                  <Button block disabled={!this.isValidForm()} onClick={this.onSubmit}>
                    Войти
                  </Button>
                </CardFooter>
              </Card>
            </Form>
          </Col>
        </Row>
      </Container>
    );
  }
}

const provider = provide(
  (state: IApplicationState) => (
    {
      ...state.user,
    }
  ),
  {
    ...actionCreators,
  },
).withExternalProps<IExternalProps>();

type Props = typeof provider.allProps;

export default provider.connect(Login);
