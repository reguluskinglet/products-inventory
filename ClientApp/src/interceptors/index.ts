import { useHttpRequestInterceptor } from './HttpRequestInterceptor';

export const useInterceptors = () => {
  useHttpRequestInterceptor();
};
