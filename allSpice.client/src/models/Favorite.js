export class Favorite {
  constructor(data) {
    this.id = data.id;
    this.favoriteId = data.favoriteId;
    this.title = data.title;
    this.instructions = data.instructions;
    this.img = data.img;
    this.category = data.category;
    this.creator = data.creator;
  }
}