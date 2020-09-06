import { History } from 'history';
import { routerMiddleware, connectRouter } from 'connected-react-router';
import {
    applyMiddleware,
    combineReducers,
    compose,
    createStore,
    Middleware,
    ReducersMapObject,
} from 'redux';
import reduxThunk from 'redux-thunk';
import { typedToPlain } from 'redux-typed';
import { IApplicationState, reducers } from './store';

function buildRootReducer(allReducers: ReducersMapObject, history: History) {
    return combineReducers(
        Object.assign({}, allReducers, { router: connectRouter(history) }),
    );
}

export default function configureStore(
    history: History,
    initialState: IApplicationState = {} as IApplicationState,
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