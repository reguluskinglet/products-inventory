/* eslint-disable global-require */
/* eslint-disable prefer-object-spread */
import { History } from 'history';
import { routerMiddleware, connectRouter } from 'connected-react-router';
import {
  applyMiddleware,
  combineReducers,
  compose,
  createStore,
  ReducersMapObject,
} from 'redux';
import reduxThunk from 'redux-thunk';
import { typedToPlain } from 'redux-typed';
import { ApplicationState, reducers } from './store';

function buildRootReducer(allReducers: ReducersMapObject, history: History) {
  return combineReducers<ApplicationState>(
    Object.assign({}, allReducers, { router: connectRouter(history) }),
  );
}

export default function configureStore(
  history: History,
  initialState: ApplicationState = {} as ApplicationState,
) {
  const composeEnhancers = (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

  const createStoreWithMiddleware = composeEnhancers(
    applyMiddleware(
      reduxThunk,
      typedToPlain,
      routerMiddleware(history),
    ),
  )(createStore);

  const allReducers = buildRootReducer(reducers, history);
  const store = createStoreWithMiddleware(allReducers, initialState);

  // Webpack
  if ((module as any).hot) {
    (module as any).hot.accept('./store', () => {
      const nextRootReducer = require('./store');
      store.replaceReducer(buildRootReducer(nextRootReducer.reducers, history));
    });
  }

  return store;
}
