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
  FormTextarea,
  Button,
  Nav,
  NavItem,
  NavLink
} from 'shards-react';

import './styles.scss';
import { ImPriceTag } from 'react-icons/im';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class AddProduct extends React.Component<Props, any> {

  constructor(props: any) {
    super(props);

    this.state = {
      title: { value: ''},
      description: { value: ''},
      price: { value: ''},
    };
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

    const { addProduct } = this.props;
    const { title, description, price } = this.state;

    if (this.isValidForm()) {
      addProduct(
        {
          title: title.value,
          description: description.value,
          price: price.value
        },
      );
    }
  };

  private validate = (value: string) => {
    return value && value.length > 0;
  }

  private isValidForm = () => {
    const { title, description, price } = this.state;
    return this.validate(title.value) && this.validate(description.value) && this.validate(price.value);
  };

  public render() {
    return (
      <Container className="add-product-container">
        <Nav pills>
          <NavItem>
            <NavLink href="#/products">Список Товаров</NavLink>
          </NavItem>
          <NavItem>
            <NavLink active href="#/add-product">Добавить товар</NavLink>
          </NavItem>
        </Nav>
        <Row>
          <Col>
            <Form>
              <Card>
                <CardHeader>Добавить товар</CardHeader>
                <CardBody>
                  <FormGroup>
                    <label>Наименование</label>
                    <FormInput onChange={(e: any) => this.onChange('title', e.target.value)} />
                  </FormGroup>
                  <FormGroup>
                    <label>Описание</label>
                    <FormTextarea onChange={(e: any) => this.onChange('description', e.target.value)} />
                  </FormGroup>
                  <FormGroup>
                    <label>Цена</label>
                    <FormInput type="number" onChange={(e: any) => this.onChange('price', e.target.value)} />
                  </FormGroup>
                </CardBody>
                <CardFooter>
                  <Button disabled={!this.isValidForm()} onClick={this.onSubmit}>
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
