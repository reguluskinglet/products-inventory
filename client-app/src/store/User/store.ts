import { isActionType, Reducer } from 'redux-typed';
import { IUserInfo } from '../../common/Contracts/IUserInfo';
import { LoginUserAction } from './action';

export interface IUserState {
    errorMessage?: string;
    user: IUserInfo;
}

export const reducer: Reducer<IUserState> = (state, action: any) => {
    if (isActionType(action, LoginUserAction)) {
        return {
            ...state,
            usersLoading: action.user,
        };
    }

    return state || { user: null, errorMessage: null}
}
