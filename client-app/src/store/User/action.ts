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
    logIn: (credentials: ILoginModel, redirectTo: string) => (dispatch: any, getState: any) => {
        MyAxiosFetch.post(`${Config.SERVICE_URL}/api/users/login`, credentials)
            .then(async (response: any) => {
                if (response.access_token) {
                    //UserService.auth(response);

                    //const currentUser = await UserService.getUser();
                    //dispatch(new LoginUserAction(currentUser));
                    //dispatch(push(redirectTo));
                }
            })
            .catch(() => {
                Logger.error("Ошибка при попытке авторизоваться в систему.")
            });
    },
}
