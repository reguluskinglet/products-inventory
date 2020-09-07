import Logger from 'loglevel';
import store from '..';
import { Config } from '../config';
import MyAxiosFetch from '../interceptors/MyAxiosFetch';
import { IProductsPage } from '../common/Contracts/IProductsPage';
import { IProduct } from '../common/Contracts/IProduct';

export class ProductService {
  public static getProductsPage = async (pageIndex: number, pageSize: number): Promise<IProductsPage> => {
    const productsPage = await MyAxiosFetch.get(`${Config.SERVICE_URL}/api/products/page`, {
      params: {
        pageSize,
        pageIndex,
      },
    })
      .then((response: any) => response)
      .catch((error) => {
        Logger.error('Ошибка во время получения станицы товаров: %o', error);
      });

    return productsPage;
  };

  public static addProduct = async (product: IProduct): Promise<any> => {
    const productResult = await MyAxiosFetch.post(`${Config.SERVICE_URL}/api/products/add`, product)
        .then((response: any) => response)
        .catch((error) => {
            Logger.error('Ошибка во время создания товара: %o', error);
        });
    return productResult;
  };
}