export interface AppMessage {
  text: string;
  type: 'success' | 'danger' | 'warning' | 'info';
}
