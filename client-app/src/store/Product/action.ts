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


@typeName('GetProductsAction')
export class GetProductsAction extends Action {
    constructor(
        public data: IProduct,
        public totalCount: number,
    ) {
        super();
    }
}

export const actionCreators = {
    getProductsPage: (pageIndex: number, pageSize: number) => (dispatch: any, getState: any) => {

    },
}
