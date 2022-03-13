import RESTAdapter from '@ember-data/adapter/rest';
import config from '../config/environment';

export default class ApplicationAdapter extends RESTAdapter {
  host = config.EndPoints.Api.host;
  namespace = config.EndPoints.Api.namespace;

  handleResponse(status, headers, payload, requestData) {
    if (this.isInvalid(...arguments)) {
      window.snackbar("error", "An error occurred while processing your request. Our support team has been notified, and will fix the problem as soon as possible. We apologise for the inconvenience.");
      
      return new InvalidError({
          status
      });
    }

    return payload;
  }
}
