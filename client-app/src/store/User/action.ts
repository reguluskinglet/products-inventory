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
import { UserService } from '../../services';


@typeName('LoginUserAction')
export class LoginUserAction extends Action {
    constructor(
        public user: IUserInfo,
        public errorMessage?: string,
    ) {
        super();
    }
}

export const actionCreators = {
    logIn: (credentials: ILoginModel, redirectTo: string) => async (dispatch: any, getState: any) => {
        await MyAxiosFetch.post(`${Config.SERVICE_URL}/api/users/login`, credentials)
            .then(async (response: any) => {
                if (response.access_token) {
                    UserService.auth(response);

                    const currentUser = await UserService.getUser();
                    dispatch(new LoginUserAction(currentUser));
                    dispatch(push(redirectTo));
                }
            })
            .catch((error: any) => {
                Logger.error("Ошибка при попытке авторизоваться в систему.", error)
            });
    },
}
