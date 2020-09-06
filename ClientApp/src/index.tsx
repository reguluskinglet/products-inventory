import { createHashHistory as createHistory } from 'history';
import React from 'react';
import ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import configureStore from './configureStore';
import { useInterceptors } from './interceptors';
import * as RoutesModule from './routes';
import * as serviceWorker from './serviceWorker';
import { ApplicationState } from './store';

import 'bootstrap/dist/css/bootstrap.min.css';

export const history = createHistory();

const initialState = (window as any).initialReduxState as ApplicationState || {};
const store = configureStore(history, initialState);

function renderApp() {
  ReactDOM.render(
    <AppContainer>
      <Provider store={store}>
        <ConnectedRouter history={history}>
          {RoutesModule.routes}
        </ConnectedRouter>
      </Provider>
    </AppContainer>,
    document.getElementById('root'),
  );
}

useInterceptors();

renderApp();

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();

export default store;
