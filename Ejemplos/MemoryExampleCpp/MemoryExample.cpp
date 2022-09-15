#include <iostream>

struct GameObject {
	float x;
	float y;
};

void print(GameObject* obj) {
	std::cout << "x: " << obj->x << ", ";
	std::cout << "y: " << obj->y << "\n";
}

GameObject sword;

int main() {
	sword.x = 5;
	sword.y = 7;
	print(&sword);

	GameObject hero;
	hero.x = 10;
	hero.y = 42;
	print(&hero);

	GameObject* enemy = new GameObject();
	enemy->x = 15;
	enemy->y = 30;
	print(enemy);
	delete enemy;
}