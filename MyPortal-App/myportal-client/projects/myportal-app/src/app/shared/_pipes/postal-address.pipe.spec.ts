import { PostalAddressPipe } from './postal-address.pipe';

describe('PostalAddressPipe', () => {
  it('create an instance', () => {
    const pipe = new PostalAddressPipe();
    expect(pipe).toBeTruthy();
  });
});
