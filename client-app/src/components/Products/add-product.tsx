import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import { IApplicationState } from '../../store';
import { actionCreators } from '../../store/Product/action';
import {
  Container,
  Row,
  Col,
  Card,
  CardHeader,
  CardImg,
  CardTitle,
  CardBody,
  CardFooter,
  Form,
  FormGroup,
  FormInput,
  FormTextArea,
  Button,
  Nav,
  NavItem,
  NavLink
} from 'shards-react';

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class AddProduct extends React.Component<Props, any> {
  public render() {
    return (
      <Container className="add-product-container">
        <Row>
          <Nav>
            <NavItem>
              <NavLink active href="#/products">Список Товароа</NavLink>
            </NavItem>
            <NavItem>
              <NavLink active href="#/add-product">Добавить товар</NavLink>
            </NavItem>
          </Nav>
        </Row>
        <Row>
          <Col>
            <Form>
              <Card>
                <CardHeader>Добавить товар</CardHeader>
                <CardBody>
                  <FormGroup>
                    <label>Наименование</label>
                    <FormInput placeholder="" onChange={(e) => this.onChange('login', e.target.value)} />
                  </FormGroup>
                  <FormGroup>
                    <label>Описание</label>
                    <FormTextArea placeholder="" onChange={(e) => this.onChange('password', e.target.value)} />
                  </FormGroup>
                </CardBody>
                <CardFooter>
                  <Button disabled={!this.isValidForm} onClick={this.onSubmit}>
                    Добавить товар
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
      ...state.products,
    }
  ),
  {
    ...actionCreators,
  },
).withExternalProps<IExternalProps>();
  
type Props = typeof provider.allProps;

export default provider.connect(AddProduct);
