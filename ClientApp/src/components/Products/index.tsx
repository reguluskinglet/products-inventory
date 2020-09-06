import React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { provide } from 'redux-typed';
import { ApplicationState } from '../../store';
import * as ProductStore from '../../store/ProductStore';

import './styles.scss';

interface IExternalProps extends RouteComponentProps<any> {
  properties: any;
}

class Product extends React.Component<Props, any> {
  public render() {
    return {};
  }
}

const provider = provide(
  (state: ApplicationState) => (
    {
      ...state.product,
    }
  ),
  {
    ...ProductStore.actionCreators,
  },
).withExternalProps<IExternalProps>();
  
type Props = typeof provider.allProps;

export default provider.connect(Product);
