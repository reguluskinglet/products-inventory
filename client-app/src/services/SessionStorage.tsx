
import { IUserInfo } from '../common/Contracts/IUserInfo';

export default class SessionStorage {
  private static tokenKey = 'operator';

  public static getToken() {
    return localStorage.getItem(this.tokenKey);
  }

  public static setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  public static removeToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  public static setUserInfo(data: IUserInfo): void {
    this.setObject(this.tokenKey, data);
  }

  public static getUserInfo(): IUserInfo {
    return this.getObject(this.tokenKey);
  }

  private static getObject = (key: string) => JSON.parse(localStorage.getItem(key) as any);

  private static setObject = (key: string, value: any) => {
    localStorage.setItem(key, JSON.stringify(value));
  };
}
