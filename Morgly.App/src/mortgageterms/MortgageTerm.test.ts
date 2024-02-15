import { multiply } from './MortgageTerm';

describe('multiply', () => {
  it('should multiply two numbers correctly', () => {
    expect(multiply(2, 3)).toBe(6);
    expect(multiply(5, 0)).toBe(0);
    expect(multiply(-4, 8)).toBe(-32);
  });
});