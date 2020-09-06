import React from 'react';
import { Redirect, Route } from 'react-router-dom';
import { routePaths } from './constants';
import { UserService } from '../services';

export const AuthComponent = (
  {
    component: Component,
    ...rest
  }: { component: any, path: string, exact?: boolean },
) => (
  <Route
    {...rest}
    render={(props) => (
      UserService.IsAuthenticated
        ? <Component {...props} />
        : (
          <Redirect
            to={{
              pathname: routePaths.login,
              state: { from: props.location },
            }}
          />
        )
    )}
  />
);
