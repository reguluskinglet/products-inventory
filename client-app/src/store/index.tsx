import { ActionCreatorGeneric } from 'redux-typed';
import * as UserStore from './User/store';
import * as ProductStore from './Product/store';

export interface IApplicationState {
    user: UserStore.IUserState;
    products: ProductStore.IProductState;
}

export const reducers = {
    user: UserStore.reducer,
    products: ProductStore.reducer,
};

export type ActionCreator = ActionCreatorGeneric<IApplicationState>;
