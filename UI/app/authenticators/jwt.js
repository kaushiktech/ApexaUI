import Base from 'ember-simple-auth/authenticators/base';

export default Base.extend({
  async restore(data) {
    let { token } = data;
    if (token) {
      return data;
    } else {
      throw 'no valid session data';
    }
  },
  async authenticate(username, password) {
    let response = await fetch(
      'https://localhost:7019/api/Authenticate/login',
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: username,
          password: password,
        }),
      },
    );
    if (response.ok) {
      return response.json();
    } else {
      let error = await response.json();
      throw new Error(error.title);
    }
  },
  async invalidate(data) {},
});
