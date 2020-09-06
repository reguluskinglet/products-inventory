/* eslint-disable max-classes-per-file */
import {
  Action, isActionType, Reducer,
  typeName,
} from 'redux-typed';
import axios from 'axios';
import { RequestEvent } from '../common';
import { UserService } from '../services';
import { IUserInfo } from '../common/contracts/IUserInfo';
import { ILoginModel } from '../common/contracts/ILoginModel';
import { Config } from '../config';

export interface IUserState {
  requestEvent: RequestEvent;
  errorMessage?: string;
  user: IUserInfo;
  usersLoading: boolean;
  usersError: string;
}

@typeName('LoginUserAction')
class LoginUserAction extends Action {
  constructor(
    public requestEvent = RequestEvent.None,
    public user: IUserInfo,
    public errorMessage?: string,
  ) {
    super();
  }
}

@typeName('GetUserAction')
// eslint-disable-next-line @typescript-eslint/no-unused-vars
class GetUserAction extends Action {
  constructor(
    public requestEvent = RequestEvent.None,
    public user: IUserInfo,
    public errorMessage?: string,
  ) {
    super();
  }
}

export const actionCreators = {
  logIn: (credentials: ILoginModel, redirectTo: string) => (dispatch, getState) => {
    axios.post(`${Config.SERVICE_URL}/api/users/login`, credentials)
      .then(async (response: any) => {
        if (response.access_token) {
          UserService.auth(response);

          const currentUser = await UserService.getUser();
          dispatch(new LoginUserAction(RequestEvent.End, currentUser));
        }
      })
      .catch(() => { });
  }
}

export const reducer: Reducer<IUserState> = (state, action: any) => {
  if (isActionType(action, LoginUserAction)) {
    return { ...state, user: action.user };
  }

  return state || { data: {} };
}
