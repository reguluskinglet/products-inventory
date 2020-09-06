/* eslint-disable no-param-reassign */
/* eslint-disable max-classes-per-file */
import {
  Action, isActionType, Reducer,
  typeName,
} from 'redux-typed';

export interface IProductState {
  data: any | null
}

@typeName('GetProductsAction')
class GetProductsAction extends Action {
  constructor(
    public data: any,
  ) {
    super();
  }
}

export const actionCreators = {
  getProducts: () => (dispatch) => {
    dispatch(new GetProductsAction(null));
  },
};

export const reducer: Reducer<IProductState> = (state, action: any) => {
  if (isActionType(action, GetProductsAction)) {
    return { ...state, data: action.data };
  }

  return state || { data: {} };
};
