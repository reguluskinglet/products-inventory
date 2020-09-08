import React from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import { PrivateLayout } from './components/Layout';
import Products from './components/Products';
import AddProduct from './components/Products/add-product';
import Login from './components/Login';
import { AuthComponent } from './common/AuthComponent';
import { routePaths } from './common/constants';

const Private = () => (
  <PrivateLayout>
    <Switch>
      <Route exact path={routePaths.home} render={() => <Redirect to={routePaths.products} />} />
      <Route exact path={routePaths.products} render={() => <Redirect to={routePaths.login} />} />
    </Switch>
  </PrivateLayout>
);

export const routes = (
  <Switch>
    <Route path={routePaths.login} component={Login} />
    <Route path={routePaths.products} component={Products} />
    <Route path={routePaths.addProduct} component={AddProduct} />
    <AuthComponent path={routePaths.home} component={Private} />
  </Switch>
);
