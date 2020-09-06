import React from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import { AuthComponent, routePaths } from './common';
import {
  PrivateLayout,
  Login,
  Products,
} from './components';

const Private = () => (
  <PrivateLayout>
    <Switch>
      <Route exact path={routePaths.products} render={() => <Redirect to={routePaths.login} />} />
    </Switch>
  </PrivateLayout>
);

export const routes = (
  <Switch>
    <Route path={routePaths.login} component={Login} />
    <Route path={routePaths.products} component={Products} />
    <AuthComponent path={routePaths.home} component={Private} />
  </Switch>
);
