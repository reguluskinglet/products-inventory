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
      pageIndex: 1,
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
      }
    });
  }

  public async componentDidMount() {
    const { pageSize, pageIndex } = this.state;
    await this.loadProducts(pageIndex, pageSize);
  }

  private onPageChange = async (pageIndex: number) => {
    const { pageSize } = this.state;
    await this.loadProducts(pageIndex, pageSize);
  };

  private loadProducts = async (pageIndex: number, pageSize: number) => {
    const { getProductsPage } = this.props;
    
    await getProductsPage(pageIndex, pageSize);

    const totalPages = Math.ceil(this.props.totalCount / pageSize);
    this.setState({
      pageIndex: pageIndex,
      hasNextPage: pageIndex < totalPages,
      hasPreviousPage: pageIndex > 1,
      totalPages: totalPages,
      totalCount : this.props.totalCount,
    });
  }

  private getPageInfoString = (): string => {
    const { totalCount } = this.props;
    const { pageSize, pageIndex, totalPages } = this.state;

    const from = totalPages ? ( pageIndex - 1) * pageSize + 1 : 0;
    const to = pageIndex * pageSize;
    return `${from}-${to} - ${totalCount}`;
  };

  private appendProducts = async (pageIndex: number, pageSize: number) => {
    const { appendProductsPage } = this.props;
    await appendProductsPage(pageIndex, pageSize);

    const totalPages = Math.ceil(this.props.totalCount / pageSize);
    this.setState({
      pageIndex: pageIndex,
      hasNextPage: pageIndex < totalPages,
      hasPreviousPage: pageIndex > 1,
      totalPages: totalPages,
      totalCount : this.props.totalCount,
    });
  }

  public render() {
    const { totalCount, products } = this.props;
    const { pageSize, pageIndex, hasNextPage, hasPreviousPage, totalPages } = this.state;
    return (
      <Container className="products-container">
        <Nav pills>
          <NavItem>
            <NavLink active href="#/products">Список Товароа</NavLink>
          </NavItem>
          <NavItem>
            <NavLink href="#/add-product">Добавить товар</NavLink>
          </NavItem>
        </Nav>
        <Row>
          <Col>
            <div className="buttons-wrapper">
            <Button
              disabled={!pageIndex || pageIndex === 1}
              onClick={() => this.onPageChange(1)}
            >
              <ImUndo/>
            </Button>
            <Button
              disabled={!hasPreviousPage}
              onClick={() => this.onPageChange(pageIndex - 1)}
            >
              <ImArrowLeft2/>
            </Button>
            <span className="navigation-info">{this.getPageInfoString()}</span>
            <Button
              disabled={!hasNextPage}
              onClick={() => this.onPageChange(pageIndex + 1)}
            >
              <ImArrowRight2/>
            </Button>
            <Button
              disabled={pageIndex * pageSize >= totalCount}
              onClick={() => this.onPageChange(totalPages)}
            >
              <ImRedo/>
            </Button>
          </div>
          </Col>
        </Row>
        <Row>
        {products.map((product: IProduct, index: number) => (
          <Col key={index} sm="12" lg="4">
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
