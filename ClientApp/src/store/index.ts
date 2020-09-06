import { ActionCreatorGeneric } from 'redux-typed';
import * as ProductStore from './ProductStore';
import * as UserStore from './UserStore';

export interface ApplicationState {
    user: UserStore.IUserState;
    product: ProductStore.IProductState;
}

export const reducers = {
    user: UserStore.reducer,
    product: ProductStore.reducer,
};

export type ActionCreator = ActionCreatorGeneric<ApplicationState>;
