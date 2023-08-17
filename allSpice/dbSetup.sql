CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(1000) NOT NULL,
  img VARCHAR(700) NOT NULL,
  category ENUM ('Cheese','Italian','Soup','Mexican','Specialty Coffee'),
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE ingredients(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT Null,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE favorites(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  recipeId INT NOT NULL,
  accountId VARCHAR(255) NOT Null,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE recipes

DROP TABLE ingredients

DROP TABLE favorites

INSERT INTO recipes(title, instructions, img, category, creatorId)
VALUES ('Hot Dogs','Microwave, Apply condements, Add bun, Eat','Valid IMG URL', 'Italian','64dd35fd2635444c1cf615ee' )

INSERT INTO ingredients(name, quantity, recipeId, creatorId)
VALUES ('Hot Dog','One Whole Cooked Weiner', 1, '64dd35fd2635444c1cf615ee')

SELECT * FROM ingredients WHERE id = 1;

INSERT INTO favorites(recipeId, accountId)
VALUES ('1','64dd35fd2635444c1cf615ee')