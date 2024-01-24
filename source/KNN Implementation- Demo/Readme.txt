KNN CLassifier (DEMO)

Knn classifier is used for classification prolem, predict a class or label based on the predictor value. Simple and elgant approach for classification relative to other ML classification technique
like naive Bayes or Neural Network Classification. KNN classifier advantage is Flexibility and Simplicity.


Example:

Class: Server falling ( Low, Medium , High)
Predictor Value: (HTM reuqest handled, Physical temp)

Drawbacks:

1. unstatisfying result on Non Numeric Predictor Values
2. Not a work efficiently on Large Dataset

--------------------------------------------------
Thing to remember while Applying KNN

1. Use Normalization technique for data (by dividing with n number). Two common normalization technique used are z-score normalization and min-max normalization.
2. Two technique are usually used to predict a class, one is based on distance method and other is based on voting method between classes whom distance from predictor value is almost same.
3. There are no fixed rules to figure out the best value for k in k-NN classification. The choice of k is a hyperparameter, and you have to rely on intuition and experimentation to find
the right value.

All the step of Algo
1. Compute distances from unknown to all training items
2. Sort the distances from nearest to farthest
3. Scan the k-nearest items; use a vote to determine the result