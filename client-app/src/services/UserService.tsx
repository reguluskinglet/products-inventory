import { SessionStorage } from '.';
import { IUserInfo } from '../common/Contracts/IUserInfo';

export default class UserService {
  static get IsAuthenticated(): boolean {
    return SessionStorage.getToken() !== null;
  }

  public static getUser(): IUserInfo {
    return SessionStorage.getUserInfo();
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
    SessionStorage.setUserInfo(userSession);
    return userSession;
  }
}
