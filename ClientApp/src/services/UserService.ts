import { Storage } from './';
import { IUserInfo } from '../common/contracts/IUserInfo';

export default class UserService {
  static get IsAuthenticated(): boolean {
    return Storage.getToken() !== null;
  }

  public static getUser(): IUserInfo {
    return Storage.getUserInfo();
  }

  public static getUserId(): string {
    const user = UserService.getUser();
    return user.id;
  }

  public static auth(data: any): IUserInfo {
    const userSession: IUserInfo = {
      access_token: data.access_token,
      id: data.id,
    };
    Storage.setUserInfo(userSession);
    return userSession;
  }
}
