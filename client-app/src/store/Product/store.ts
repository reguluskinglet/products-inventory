import { isActionType, Reducer } from 'redux-typed';
import { IProduct } from '../../common/Contracts/IProduct';
import { GetProductsAction, AppendProductsAction } from './action';

export interface IProductState {
    products: IProduct[];
    totalCount: number;
}

export const reducer: Reducer<IProductState> = (state: any, action: any) => {
    if (isActionType(action, GetProductsAction)) {
        if(action.totalCount === 0) {
            return { ...state, products: action.data };
        } else {
            return { ...state, products: action.data, totalCount: action.totalCount };
        }
    }

    if (isActionType(action, AppendProductsAction)) {
        const appendedProducts = state.products.concat(action.data);
        if(action.totalCount === 0) {
            return { ...state, products: appendedProducts };
        } else {
            return { ...state, products: appendedProducts, totalCount: action.totalCount };
        }
    }

    return state || { products: [], totalCount: 0 }
}
