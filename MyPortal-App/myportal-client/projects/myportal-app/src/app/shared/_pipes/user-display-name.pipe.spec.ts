import { UserDisplayNamePipe } from './user-display-name.pipe';

describe('UserDisplayNamePipe', () => {
  it('create an instance', () => {
    const pipe = new UserDisplayNamePipe();
    expect(pipe).toBeTruthy();
  });
});
