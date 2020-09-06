import axios from 'axios';
import Logger from 'loglevel';
import { Storage } from '../services';

export const useHttpRequestInterceptor = () => {
  const axiosInstance = axios.create();
  axiosInstance.interceptors.request.use((config) => {
    const userInfo = Storage.getUserInfo();
    if (userInfo) {
      const newConfig = {
        ...config,
        headers: {
          ...config.headers,
          Authorization: `Bearer ${userInfo.access_token}`,
        },
      };

      return newConfig;
    }

    Logger.info('UserInfo не найдено. UserId не добавлен в хэдер запроса');
    return config;
  });
};
