
import MyAxiosFetch from './MyAxiosFetch';
import SessionStorage from '../services/SessionStorage';

export const useHttpRequestInterceptor = () => {
  MyAxiosFetch.interceptors.request.use((config: { headers: any; }) => {
    const userInfo = SessionStorage.getUserInfo();
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
    return config;
  });
};
