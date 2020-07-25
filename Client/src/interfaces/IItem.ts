
enum ItemTypes {
  JOB,
  STORY,
  COMMENT,
  POLL,
  POLLOPT
}


export interface IItem {
  id: number;
  deleted: boolean;
  type: ItemTypes;
  by: string;
  time: number;
  text: string;
  dead: boolean;
  parent: string;
  poll: any;
  kids: number[];
  url: URL;
  score: number;
  title: string;
  parts: number[];
  descendents: number;

}
