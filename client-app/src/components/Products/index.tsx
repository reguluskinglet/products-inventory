import _ from 'lodash';
import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import { IApplicationState } from '../../store';
import { actionCreators } from '../../store/Product/action';
import { IProduct } from '../../common/Contracts/IProduct';
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
  Button,
  Nav,
  NavItem,
  NavLink
} from 'shards-react';
import { ImUndo, ImRedo, ImArrowLeft2, ImArrowRight2 } from "react-icons/im";

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class Product extends React.Component<Props, any> {
  state: {
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    pageSize: number;
    pageIndex: number;
    totalCount: number;
    totalPages: number;
  };

  constructor(props: any) {
    super(props);
    this.state = {
      hasNextPage: false,
      hasPreviousPage: false,
      pageSize: 9,
      pageIndex: 0,
      totalCount: 0,
      totalPages: 0,
    };

    window.onscroll = _.debounce(() => {
      const {hasNextPage, pageSize, pageIndex } = this.state;
      if (
        window.innerHeight + document.documentElement.scrollTop
        === document.documentElement.offsetHeight
        && hasNextPage
      ) {
        
        this.appendProducts(pageIndex + 1, pageSize);

        this.setState({pageIndex: pageIndex + 1});
      }
    }, 100);
  }

  public async componentDidMount() {
    const { pageSize } = this.state;
    this.loadProducts(1, pageSize);
  }

  private getPageInfoString = (): string => {
    const { totalCount } = this.props;
    const { pageSize, pageIndex, totalPages } = this.state;

    const from = totalPages ? (pageIndex - 1) * pageSize + 1 : 0;
    const to = pageIndex * pageSize;

    return `${from}-${to > totalCount ? totalCount : to} - ${totalCount}`;
  };

  private onPageChange = (pageIndex: number) => {
    const { pageSize } = this.state;

    this.loadProducts(pageIndex, pageSize);

    const totalPages = Math.ceil(this.props.totalCount / pageSize);
    this.setState({
      pageIndex: pageIndex + 1,
      hasNextPage: pageIndex < totalPages,
      hasPreviousPage: pageIndex > 1,
      totalCount: this.props.totalCount,
      totalPages: totalPages,
    });
  };

  private loadProducts = async (pageIndex: number, pageSize: number) => {
    const { getProductsPage } = this.props;
    getProductsPage(pageIndex, pageSize)
  }

  private appendProducts = async (pageIndex: number, pageSize: number) => {
    const { appendProductsPage } = this.props;
    appendProductsPage(pageIndex, pageSize)
  }

  public render() {
    const { totalCount, products } = this.props;
    const { pageSize, pageIndex, hasNextPage, hasPreviousPage, totalPages } = this.state;
    return (
      <Container className="products-container">
        <Row>
          <Nav>
            <NavItem>
              <NavLink active href="#/products">Список Товароа</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="#/add-product">Добавить товар</NavLink>
            </NavItem>
          </Nav>
        </Row>
        <Row>
          <Col>
            <div className="buttons-wrapper">
            <Button
              disabled={!pageIndex || pageIndex === 1}
              onClick={() => this.onPageChange(1)}
            >
              <ImUndo width="16" height="16" fill="#3D4551" />
            </Button>
            <Button
              disabled={!hasPreviousPage}
              onClick={() => this.onPageChange(pageIndex - 1)}
            >
              <ImArrowLeft2 width="16" height="16" fill="#3D4551" />
            </Button>
            <span className="navigation-info">{this.getPageInfoString()}</span>
            <Button
              disabled={!hasNextPage}
              onClick={() => this.onPageChange(pageIndex + 1)}
            >
              <ImArrowRight2 width="16" height="16" fill="#3D4551" />
            </Button>
            <Button
              disabled={!totalPages || pageIndex === totalCount}
              onClick={() => this.onPageChange(totalPages)}
            >
              <ImRedo width="16" height="16" fill="#3D4551" />
            </Button>
          </div>
          </Col>
        </Row>
        <Row>
        {products.map((product: IProduct) => (
          <Col>
              <Card>
              <CardImg style={{width: "100%"}} src="https://cdn.shopify.com/s/files/1/0095/7510/4576/products/Signet-Ring-9_2048x2048.jpg?v=1569415408" />
              <CardBody>
                <CardTitle>{product.title}</CardTitle>
                <p>{product.description}</p>
                <p>${product.price}</p>
              </CardBody>
            </Card>
          </Col>
        ))}
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

export default provider.connect(Product);
