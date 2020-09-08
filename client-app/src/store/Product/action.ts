import {
    Action,
    typeName,
} from 'redux-typed';
import { push } from 'connected-react-router';
import Logger from 'loglevel';
import { IUserInfo } from "../../common/Contracts/IUserInfo";
import MyAxiosFetch from '../../interceptors/MyAxiosFetch';
import { ILoginModel } from '../../common/Contracts/ILoginModel';
import { Config } from '../../config';
import { IProduct } from '../../common/Contracts/IProduct';
import { ProductService } from '../../services/ProductService';


@typeName('GetProductsAction')
export class GetProductsAction extends Action {
    constructor(
        public data: IProduct[],
        public totalCount: number,
    ) {
        super();
    }
}

@typeName('GetProductsAction')
export class AppendProductsAction extends Action {
    constructor(
        public data: IProduct[],
        public totalCount: number,
    ) {
        super();
    }
}

export const actionCreators = {
    getProductsPage: (pageIndex: number, pageSize: number) => async (dispatch: any, getState: any) => {
        var result = await ProductService.getProductsPage(pageIndex, pageSize);
        if(result && result.products) {
            dispatch(new GetProductsAction(result.products, result.totalCount));
        }
    },

    appendProductsPage: (pageIndex: number, pageSize: number) => async (dispatch: any, getState: any) => {
        var result = await ProductService.getProductsPage(pageIndex, pageSize);
        if(result && result.products) {
            dispatch(new AppendProductsAction(result.products, result.totalCount));
        }
    },

    addProduct: (product: IProduct) => async (dispatch: any, getState: any) => {
        await ProductService.addProduct(product);
        dispatch(push("/products"));
    },
}
